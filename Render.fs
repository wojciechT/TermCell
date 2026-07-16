namespace TermCell

open System
open GameTypes

module Render = 
// ♠♣♥♦

    let private suitToSymbol = 
        function
        | Spade -> "S"
        | Heart -> "H"
        | Diamond -> "D"
        | Club -> "C"
    
    let private rankToSymbol = 
        function
        | Ace ->    "A  "
        | Two ->    "2  "        
        | Three ->  "3  "
        | Four ->   "4  "
        | Five ->   "5  "
        | Six ->    "6  "
        | Seven ->  "7  "
        | Eight ->  "8  "
        | Nine ->   "9  "
        | Ten ->    "10 "
        | Jack ->   "J  "
        | Queen ->  "Q  "
        | King ->   "K  "

    let private cardToSymbol (card : Card) = 
        sprintf "|%s%s|" (rankToSymbol card.Rank) (suitToSymbol card.Suit)

    let private suitToColor =
        function
        | Spade | Club -> ConsoleColor.White
        | Heart | Diamond -> ConsoleColor.Red

    let renderCard (card : Card) =
        let color = Console.ForegroundColor
        Console.ForegroundColor <- suitToColor card.Suit
        Console.Write(cardToSymbol card)
        Console.ForegroundColor <- color
