# A Lonely Man

-> main

=== main ===
You encountered a lonely man! He asks if you could kindly give him a ride to the next stop.
    + [You can go with us]
        -> FirstChoice
    + [Steal from the guy]
        -> SecondChoice
    + [Flip him off]
        -> ThirdChoice


== FirstChoice ==
#reward money
He gave you money as a sign of thanks!
-> DONE

== SecondChoice ==
#reward money
You took his money and ran off!
-> DONE

== ThirdChoice ==
#harm 20
He pointed you his gun but he missed because you already started driving off.
-> DONE

-> END