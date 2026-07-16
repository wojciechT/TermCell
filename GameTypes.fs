namespace TermCell

module GameTypes = 
    type FreeCell = { PlacedCard: Card option }
    type GoalColumn = { GoalCards: Card list }
    type WorkingColumn = { WorkingCards: Card list }

    type SourceType = FreeCell of FreeCell | WorkingColumn of WorkingColumn
    type TargetType = FreeCell of FreeCell | GoalColumn of GoalColumn | WorkingColumn of WorkingColumn

    type Source' =
        | W1
        | W2
        | W3
        | W4
        | W5
        | W6
        | W7
        | W8
        | F1
        | F2
        | F3
        | F4

    type Target' =
        | W1
        | W2
        | W3
        | W4
        | W5
        | W6
        | W7
        | W8
        | F1
        | F2
        | F3
        | F4
        | G1
        | G2
        | G3
        | G4
    
    type ColumnIndex =
    | W of int
    | F of int
    | G of int

    type Move = { Source: ColumnIndex; Target: ColumnIndex }

    type FreeCells = {F1: FreeCell; F2: FreeCell; F3: FreeCell; F4: FreeCell}
    type GoalColumns = {G1: GoalColumn; G2: GoalColumn; G3: GoalColumn; G4: GoalColumn}
    type WorkingColumns = {
        W1: WorkingColumn;
        W2: WorkingColumn;
        W3: WorkingColumn;
        W4: WorkingColumn;
        W5: WorkingColumn;
        W6: WorkingColumn;
        W7: WorkingColumn;
        W8: WorkingColumn;
    }

    type GameState = 
        {
            FreeCells : Map<int, FreeCell>
            GoalColumns : Map<int, GoalColumn>
            WorkingColumns : Map<int, WorkingColumn>
        }
    with static member Empty() = { 
            FreeCells = Map [1, {PlacedCard = None}; 2, {PlacedCard = None}; 3, {PlacedCard = None}; 4, {PlacedCard = None}]; 
            GoalColumns = Map [1, {GoalCards = []}; 2, {GoalCards = []}; 3, {GoalCards = []}; 4, {GoalCards = []}];
            WorkingColumns =  Seq.init 8 (fun i -> i + 1,  {WorkingCards = []}) |> Map }
