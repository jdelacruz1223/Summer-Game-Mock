# Sushi Pit Stop
#Party true
 -> main
 
 === main ===
 You pass a roadside sushi stand and someone suggests stopping for a quick bite.
    + [Stop and buy sushi]
        -> FirstChoice
    + [Keep driving]
        -> SecondChoice
    + [Make a joke about gas station sushi]
        -> ThirdChoice
        
== FirstChoice ==
#reward food
#happyparty 10
You stop and buy some sushi. It's surprisingly good, and everyone enjoys the treat. (PARTY happiness +1, food +2)
-> DONE

== SecondChoice ==
#harmparty 5
You decide to keep driving, but everyone grumbles about missing out on the sushi. (PARTY happiness -0.5)
-> DONE

== ThirdChoice ==
#happyparty 5
You make a joke about gas station sushi being a health hazard. Everyone laughs, though they're still a bit hungry. (PARTY happiness +0.5)
-> DONE
