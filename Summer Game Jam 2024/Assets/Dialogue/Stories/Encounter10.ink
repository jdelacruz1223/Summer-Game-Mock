# Weird Hitchhiker
#Party true

 -> main
 
 === main ===
 You see a person dressed as a clown hitchhiking.
    + [Pick them up]
        -> FirstChoice
    + [Drive past]
        -> SecondChoice
    + [Throw them a balloon]
        -> ThirdChoice
        
== FirstChoice ==
#happyparty 15
The clown tells jokes and makes balloon animals the whole ride. It's bizarre but fun. (PARTY happiness +1.5)
-> DONE

== SecondChoice ==
#harmparty 5
The group is slightly disappointed. "Imagine the stories we could have had," someone mutters. (PARTY happiness -0.5)
-> DONE

== ThirdChoice ==
#happyparty 10
You throw a balloon out the window, and the clown catches it with a dramatic bow. The group's laughter echoes for miles. (PARTY happiness +1)
-> DONE
