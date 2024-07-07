# Talking Tree
#Party false

 -> main
 
 === main ===
 While driving alone, you swear you hear a tree talking to you.
    + [Stop and listen]
        -> FirstChoice
    + [Keep driving]
        -> SecondChoice
    + [Talk back to the tree]
        -> ThirdChoice
        
== FirstChoice ==
#harm 10
You stop and listen intently. It's just the wind rustling the leaves, but now you feel a bit silly and slightly unnerved. (Your sanity -1)
-> DONE

== SecondChoice ==
#reward none
You keep driving, shaking off the weird experience. Trees can't talk... right? (Nothing happens)
-> DONE

== ThirdChoice ==
#happyyou 10
You talk back to the tree, having a full-blown conversation. It's absurd, but you feel strangely amused by your own antics. (Your happiness +1)
-> DONE
