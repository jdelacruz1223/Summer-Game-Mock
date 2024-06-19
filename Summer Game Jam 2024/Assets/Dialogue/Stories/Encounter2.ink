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
Yeah, that's an ostrich on the road. (Nothing happens)
-> DONE
== SecondChoice ==
#harmparty 10
#tire -1
The ostrich curiously approaches the vehicle and starts pecking at the tires. (-1 tires, PARTY happiness -1)
-> DONE
== ThirdChoice ==
#happyparty 10
#food -2
The party offers the animal some food. The ostrich gladly takes it out of your offering hands. (-2 food, PARTY happiness +1)
-> DONE