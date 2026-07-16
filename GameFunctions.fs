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

    let private ranks = 
        [|
            Ace;
            Two;
            Three;
            Four;
            Five;
            Six;
            Seven;
            Eight;
            Nine;
            Ten;
            Jack;
            Queen;
            King;
    |]

    let private suits = [| Spade; Heart; Diamond; Club; |]
    let private getNewDeck = 
        ranks 
        |> Array.allPairs suits 
        |> Array.map (fun c -> { Suit = fst c ; Rank = snd c})

    let private shuffleInPlace (deck : Card array) =
        for i = 0 to deck.Length - 2 do
            let j = System.Random.Shared.Next deck.Length
            let temp = deck[i]
            deck[i] <- deck[j]
            deck[j] <- temp
        deck

    let private deckFolder (state : GameState, lastColumn: int) (card: Card) =
        match lastColumn with
        | i when i > 8 -> failwith "Too many columns" // TODO: Make this cleaner
        | 8 -> 1
        | i -> i+1
        |> fun i -> { 
            state with WorkingColumns = state.WorkingColumns.Add(i, { WorkingCards = card :: state.WorkingColumns[i].WorkingCards })}, i
    let private deal deck =
        deck 
        |> Seq.ofArray 
        |> Seq.fold deckFolder (GameState.Empty(), 0) 
        |> fst

    let getNewGame () =
        getNewDeck
        |> shuffleInPlace
        |> deal