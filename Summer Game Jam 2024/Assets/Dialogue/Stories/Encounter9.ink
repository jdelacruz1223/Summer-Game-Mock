# Unexpected Talent
#Party true
EXTERNAL setItem(item)

 -> main
 
 === main ===
 One of your passengers suddenly starts beatboxing.
    + [Join in with singing]
        -> FirstChoice
    + [Challenge them to a rap battle]
        -> SecondChoice
    + [Tell them to stop]
        -> ThirdChoice
        
== FirstChoice ==
#happyparty 10
Your impromptu performance has everyone laughing and clapping along. You're a hit! (PARTY happiness +1)
-> DONE

== SecondChoice ==
#happyparty 15
The rap battle is fierce but friendly. It turns out you're all hidden talents. (PARTY happiness +1.5)
-> DONE

== ThirdChoice ==
#harmparty 5
The beatboxing stops, but so does the fun. Way to kill the vibe. (PARTY happiness -0.5)
-> DONE
