namespace TermCell

open Render

module Program = 

    [<EntryPoint>]
    let main _ =
        [   {Suit = Spade; Rank = Ace}; 
            {Suit = Heart; Rank = Ace};
            {Suit = Diamond; Rank = Ace};
            {Suit = Club; Rank = Ace};
        ]
        |> List.iter renderCard
        0
