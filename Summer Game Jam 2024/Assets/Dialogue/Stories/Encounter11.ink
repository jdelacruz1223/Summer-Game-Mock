# Forgotten Birthday
#Party true
EXTERNAL setItem(item)

 -> main
 
 === main ===
 One of your passengers realizes it's their birthday today.
    + [Sing Happy Birthday]
        -> FirstChoice
    + [Pretend you forgot]
        -> SecondChoice
    + [Find a roadside cake]
        -> ThirdChoice
        
== FirstChoice ==
#happyparty 10
Everyone sings loudly and off-key. It's the thought that counts, and it makes the birthday special. (PARTY happiness +1)
-> DONE

== SecondChoice ==
#harmparty 10
You joke about forgetting, but it doesn't land well. Awkward silence follows. (PARTY happiness -1)
-> DONE

== ThirdChoice ==
~setItem("food -1")
#happyparty 15
You stop at a gas station and find a snack that resembles a cake. It's not much, but it's enough to make the day memorable. (PARTY happiness +1.5, food -1)
-> DONE
