# Roadside
#Party true

EXTERNAL setItem(item)

 -> main
 
 === main ===
 A car ahead of you is pulled over to the side with their hazard lights on. The driver seems to be waving at you.
    + [Stop and help them]
        -> FirstChoice
    + [Keep driving]
        -> SecondChoice
        
== FirstChoice ==
#happyparty 10
You stopped just in time to pull up behind them. Some large tree branches and foliage were scattered throughout the street, which was sure to cause some damage if taken at full speed. You help the other driver out to clear the road and continue on. (PARTY happiness +1)
-> DONE
== SecondChoice ==
~ setItem("tire -1")
You don't immediately see any issue with their car and decide to keep going. Immediately after passing the stopped car, a loud CRUNCH and SNAP sound is heard, and the car starts shaking. You take some time to pull over and assess.. (tire -1)
-> DONE