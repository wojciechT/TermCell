namespace TermCell

[<AutoOpen>]
module Models = 

    type Color = Black | Red
    
    type Suit = Spade | Heart | Diamond | Club
    
    type Rank = 
        | Ace 
        | Two 
        | Three 
        | Four 
        | Five 
        | Six 
        | Seven 
        | Eight 
        | Nine 
        | Ten 
        | Jack 
        | Queen 
        | King

    type Card = { Suit : Suit; Rank : Rank}
