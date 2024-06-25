# Roadside
#Party true
EXTERNAL setItem(item)

 -> main
 
 === main ===
 You pass a giant statue of a beaver holding a fish.
    + [Stop and take a photo]
        -> FirstChoice
    + [Keep driving]
        -> SecondChoice
    + [Make a joke about it]
        -> ThirdChoice
        
== FirstChoice ==
#happyparty 10
Everyone poses for a ridiculous photo with the statue. It's the highlight of the trip so far. (PARTY happiness +1)
-> DONE

== SecondChoice ==
#harmparty 5
The group is a bit disappointed you didn't stop. "We'll see the next one," you promise, though no one believes you. (PARTY happiness -0.5)
-> DONE

== ThirdChoice ==
#happyparty 5
You make a corny joke about beavers and fish. The laughter is worth it, even if it's just for the groans. (PARTY happiness +0.5)
-> DONE
