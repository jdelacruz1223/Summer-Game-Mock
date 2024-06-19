# A Lone Drifter

-> main

=== main ===
You encountered a lone man slowly walking on the side of the road. He asks if you could kindly give him a ride to the nearest rest stop.
    + [You can go with us]
        -> FirstChoice
    + [Steal from the guy]
        -> SecondChoice
    + [Flip him off]
        -> ThirdChoice


== FirstChoice ==
#reward money
#harm 10
You arrive at a gas station. As the man exits your car, he gives you a wad of cash as thanks!
-> DONE

== SecondChoice ==
#harm 10
At 60+ MPH, you attempt to grab his wallet from your car window, injuring yourself and realizing this wasn't the best idea.
-> DONE

== ThirdChoice ==
#harm 20
He flips back.
-> DONE

-> END