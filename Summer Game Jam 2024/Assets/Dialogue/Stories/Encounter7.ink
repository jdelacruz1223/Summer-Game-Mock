# Mystery Meat
#Party true
EXTERNAL setItem(item)

 -> main
 
 === main ===
 One of your passengers finds some weird meat in the cooler.
    + [Eat it]
        -> FirstChoice
    + [Throw it out]
        -> SecondChoice
    + [Ask everyone where it came from]
        -> ThirdChoice
        
== FirstChoice ==
#harmparty 10
Against all advice, the passenger eats the meat. Turns out it was just old tofu, but the taste was enough to make everyone gag. (PARTY happiness -1)
-> DONE

== SecondChoice ==
#happyparty 10
You wisely throw out the mystery meat. Everyone breathes a sigh of relief, appreciating your sensible decision. (PARTY happiness +1)
-> DONE

== ThirdChoice ==
#happyparty 5
Everyone denies knowing about the meat. It remains a mystery, but at least it brought some laughs. (PARTY happiness +0.5)
-> DONE
