namespace TermCell

module CardFunctions =
    let private rankValueMap = Map [
        Ace, 1;
        Two, 2;
        Three, 3;
        Four, 4;
        Five, 5;
        Six, 6;
        Seven, 7;
        Eight, 8;
        Nine, 9;
        Ten, 10;
        Jack, 11;
        Queen, 12;
        King, 13; ]

    let rankToValue rank = rankValueMap[rank]

    let private suitColorMap = Map [
        Spade, Black;
        Heart, Red;
        Diamond, Red;
        Club, Black;
    ]

    let suitToColor suit = suitColorMap[suit]