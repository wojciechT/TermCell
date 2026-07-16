namespace TermCell

open CardFunctions
open GameTypes

module GameFunctions =
    let private areColorsAlternating (baseCard : Card) (incomingCard : Card) = 
        baseCard.Suit 
        |> suitToColor
        |> fun c -> c <> suitToColor incomingCard.Suit

    let private isRankOneLower (baseCard: Card) (incomingCard : Card) =
        rankToValue baseCard.Rank - rankToValue incomingCard.Rank = 1

    let private canWorkingCardBePlaced baseCard incomingCard =
        areColorsAlternating baseCard incomingCard && isRankOneLower baseCard incomingCard

    let canCardBePlacedOnWorkingColumn (workingColumn : WorkingColumn) incomingCard =
        match workingColumn.WorkingCards with
        | [] -> true
        | x :: _ -> incomingCard |> canWorkingCardBePlaced x

    let private isRankOneHigher (baseCard : Card) (incomingCard : Card) =
        rankToValue incomingCard.Rank - rankToValue baseCard.Rank = 1 

    let private canGoalCardBePlaced (baseCard : Card) (incomingCard : Card) =
        isRankOneHigher baseCard incomingCard && baseCard.Suit = incomingCard.Suit 

    let canCardBePlacedOnGoalColumn (goalColumn : GoalColumn) (incomingCard : Card) =
        match goalColumn.GoalCards with
        | [] -> incomingCard.Rank = Ace
        | x :: _ -> canGoalCardBePlaced x incomingCard 

    let canCardBePlacedOnFreeCell (freeCell : FreeCell) = freeCell.PlacedCard.IsNone

    let getSourceCard (state : GameState) index =
        match index with 
        | W i -> 
            match state.WorkingColumns[i].WorkingCards with
            | [] -> Error "Can't get card from an empty column"
            | x :: xs -> Ok x
        | F i -> 
            match state.FreeCells[i].PlacedCard with
            | None -> Error "Can't get card from an empty cell"
            | Some c -> Ok c
        | G _ -> Error "Goal column cards cannot be moved"

    let getTargetColumn (state : GameState) index =
        match index with
        | W i -> state.WorkingColumns[i] |> WorkingColumn
        | F i -> state.FreeCells[i] |> FreeCell
        | G i -> state.GoalColumns[i] |> GoalColumn
