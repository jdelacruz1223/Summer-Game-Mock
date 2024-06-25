# Alien Sighting
#Party true

 -> main
 
 === main ===
 Someone swears they saw a UFO in the sky.
    + [Pull over and investigate]
        -> FirstChoice
    + [Laugh it off]
        -> SecondChoice
    + [Make alien noises]
        -> ThirdChoice
        
== FirstChoice ==
#happyparty 10
You pull over and stare at the sky together. It's probably just a plane, but the shared excitement is priceless. (PARTY happiness +1)
-> DONE

== SecondChoice ==
#harmparty 5
You all laugh, but the believer feels a bit silly and embarrassed. (PARTY happiness -0.5)
-> DONE

== ThirdChoice ==
#happyparty 10
You make silly alien noises, and soon everyone joins in. It's weird, but it's your kind of weird. (PARTY happiness +1)
-> DONE
