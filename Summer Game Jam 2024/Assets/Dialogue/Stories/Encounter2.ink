#  An Ostrich..?

 -> main
 
 === main ===
 An ostrich stands in the road.
    + [Change lanes and ignore it.]
        -> FirstChoice
    + [Slow down]
        -> SecondChoice
    + [Slow down and offer to help it.]
        -> ThirdChoice
        
== FirstChoice ==
#harm
Yeah, that's an ostrich on the road.
-> DONE
== SecondChoice ==
#harm
The ostrich curiously approaches the vehicle and starts pecking at the tires. (-1 tires, PARTY happiness -1)
-> DONE
== ThirdChoice ==
#harm
The party offers the animal some food. The ostrich gladly takes it out of your offering hands. (-2 food, PARTY happiness +1)
-> DONE