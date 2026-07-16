namespace TermCell

open GameTypes
open GameFunctions

module Moves =

    let private moveCardToFreeCell cell card =
        match canCardBePlacedOnFreeCell cell with
        | true -> { cell with PlacedCard = Some card } |> FreeCell |> Ok
        | false -> Error "Free cell is occupied"

    let private moveCardToWorkingColumn column card =
        match canCardBePlacedOnWorkingColumn column card with
        | true -> { column with WorkingCards = card :: column.WorkingCards } |> WorkingColumn |> Ok
        | false -> Error "Card can't be placed on that column"

    let private moveCardToGoalColumn column card =
        match canCardBePlacedOnGoalColumn column card with
        | true -> { column with GoalCards = card :: column.GoalCards } |> GoalColumn |> Ok
        | false -> Error "Card can't be placed on that column"

    let private moveCard target (card : Card) = 
        match target with
        | FreeCell f -> moveCardToFreeCell f card
        | WorkingColumn w -> moveCardToWorkingColumn w card
        | GoalColumn g -> moveCardToGoalColumn g card
        
    let private updateState (state : GameState) targetIndex newElement =
        match targetIndex, newElement with
        | W i, WorkingColumn w -> 
            {state with WorkingColumns = state.WorkingColumns.Add(i, w)}
        | F i, FreeCell f -> 
            {state with FreeCells = state.FreeCells.Add(i, f)}
        | G i, GoalColumn g ->
            {state with GoalColumns = state.GoalColumns.Add(i, g)}
        | _ -> failwith "Incorrect index/target combination"

    // Move: Source (WorkingColumn, FreeCell) -> Target
    let processMove (state: GameState) (move : Move) =
        let target = getTargetColumn state move.Target
                
        let sourceCard = getSourceCard state move.Source

        sourceCard
        |> Result.bind (fun c -> moveCard target c)
        |> Result.map (fun t -> updateState state move.Target t)

    // statefulMove GameState -> Move -> Result<GameState, Error>
    // Check for win condition
    // Check for lose condition
    // Check for free moves