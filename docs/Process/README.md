# Sonic Characteristics | Documentation!
***oh, dear. let's not let this be the final name for this thing, okay...?***

## Inititial Ideas | 09.01.23

###  [Say what now what now?](https://youtu.be/_Ge4_stUpqs?t=19) <sup>[1](#####1)</sup>

We have here a few initial ideas for design goals and themes to explore. Beyond that? Who knows!

### Main Concepts/Ideas
* The primary motivation is to explore the ways in which sounds can contribute to reflective game experiences. Obviously this leaves a fairly wide-open field in which to continue.
* One secondary area of interest for exploration is the "character creation" menu.

These two ideas are further examined below:

### Sound and Reflective Game Experiences
How can sound contribute to the idea of reflective game experiences (or, per Hallnäs<sup>[2](#####2)</sup> those experiences in which we focus on the ways design can "provide time for reflection through intrinsic slowness" rather than entertainment)? But, specifically, how does <em>sound</em> help? Hallnäs mentions five ways in which technology can be slow. It takes time to:
1. learn how it works
2. understand why it works the way it works
3. apply it
4. see what it is
5. find out the consequences of using it

For reflective game design, two interesting approaches arise from this. How can we use slow technology methods to cause the user to reflect on the game itself ("what does this make me think about the nature of games?") AND/OR reflect on the why ("what does this say about the greater world?") The first question seems as good a place of any to start. So what functional aspect of games can we use towards technological "envelopment" ("turning the functional perspective...into a search for mastering the technology as means of expressions, as means of expressing."<sup>[3](#####3)</sup>)?

### Character Creation Menus
Mainly because of recent experiences with the character/stat management screens of two <em>very</em> different games ([Disco Elysium](https://discoelysium.com/) and [Olli Olli World](https://store.privatedivision.com/en/game/olliolli-world)), the idea of exploring a character's sonic thumbprint is of particular interest.

<img src="https://www.gameuidatabase.com/uploads/Olli-Olli-World02102022-022721-94716.jpg" width="500px" />
<sub>via gameui.database.com</sub>

<img src="http://www.playcritically.com/wp-content/uploads/2022/06/2022033119304900-1C1B527EE3CFDB01CA6E70A2AD4A19CE.jpg" width="500px" />
<sub>via playcritically.com</sub>

<p></p>

The basic idea builds on the concepts of a previous sound installation. [zipCoda](http://www.mouseandthebillionaire.com/zipCoda/) creates a generative soundtrack for different US zip codes based on data scraped from the 2010 census. Can we do the same thing for character creation? Can the character traits be set on a sonic continuum to create an individual "theme song" for your character? And then, how might this affect how the player feels about their character? Additionally (and perhaps more immediately compelling), what kind of stand-alone experience could be made from these ideas? Could you create a David O'Reilly [Mountain](https://www.davidoreilly.com/mountain) experience where the player answers a few questions and a unique visual/soundscape is created? Could that sound/visual creation be then modified visually and/or sonically with immediate changes taking place in the other plane? This kind of audio/visual "toy" feels like a fun space to do some experimentation.

There seem to be a few possible directions:
* Textual input (questions) leading to sonic/visual "thing"
* Manipulate a visual character/thing that creates a unique song
* Manipulate sonic parameters that then creates a visible representation (character, object, diorama, etc) of that sound

Let us dwell on this for a bit.

### Notes
<sub>

##### 1 This is a giant mess. Apologies to anyone who is joining in (either out of necessity or general interest) this early in the "game"
##### 2 <em>On the Philosophy of Slow Technology</em>, Lars Hallnäs
##### 3 ibid.

</sub>

## An Idea Emerges! | 01.23.23

### Tailored Questionnaire Music
The main idea here is an extension of the initial user experience of [Please Hold](https://vimeo.com/246880360), where the answers that the player gives the system results in changes to the hold music. However, this experience will be screen based rather than analog phone based (even though it is oh so very very very tempting to revisit that darling Please Hold phone. We'll see. It's not out of the question). This doesn't answer the question of what the final visible object is from answering the questionnaire, but it does reframe it in an interesting way: rather than the questions being about the thing to be created, they end up being about the player. The hope here is that this could more easily introduce the reflective state that we are hoping for. It also introduces some new things to think about:

### Goals / Design Values / Questions
* this is extendable, which is great. We can start by building the questionnaire functionality and the accompanying musical framework, and then add to it during the design phase.
* above all, ambiguity. In the questions asked, the musical changes, the overall vibe, this should feel compelling yet confusing, rather than obvious and understandable.
* this means, for example, that a question such as "Would you rather live in the city or the country?" (which is already a dumb question) does _not_ result in the addition of city and/or nature sounds because that is beyond dumb.
* reflection, as before, is the desired outcome. Do we need to know on what specific thing the user should be reflecting on? Or is it possible to create a blanket "reflective state?"
* The mechanic of "answering questions" should be much broader than how that sounds. Micro-tasks in particular could be well-implemented here ("arrange these matchsticks in a pleasing way", "using only one color, paint a representation of your ideal day") The fluxus [puzzle boxes of George Brecht](https://www.fondazionebonotto.org/it/collection/fluxus/brechtgeorge/423.html) are an obvious precedent here.
* Is it possible to think about the questions and ensuing sounds on a triangular spectrum (like the ol' [quality/time/cost triangle](https://medium.com/contractstandards/contract-performance-metrics-the-hidden-cost-of-protracted-negotiations-2cbead6d74af)) rather than on a linear scale. This feels like it could add to the ambiguity/complexity in engaging ways (but also... maybe it's too fussy? Or difficult!?)

### Important Note
One thing that should be mentioned here, but hasn't come up yet: Another goal of this project is to try three different music/sound systems with the same visual/ludic element. So the same game, but three sound treatments in three completely different frameworks, which are:
1. FMOD
2. routing the sound out via midi to Ableton Live
3. routing the sound out to an external Analog Synthesizer, using MaxMSP as the bridge

### Sounds Fun! Let's Get Building!

## Sh'okay! Sh'okay! | 01.26.23

### An Idea Tweak. A _Great_ Idea Tweak

In a discussion with [RK](https://www.rillakhaled.com/) yesterday the _problem of what the final thing of this even is_ arises (a certificate, a date, a robot, an outfit, etc), and she says "couldn't the thing that they get just be the music?"

Oho!

So, working from that insight, we begin to narrow the focus a little. The user answers a series of questions (or does some alternative fluxusy tasks as noted above), and as they progress the music is slowly evolving and adapting in the background. Meanwhile, under the hood, the program is recording the entire experience so that when you are done you are delivered a recording of the X number of minutes that you spent on the process. It almost becomes a sort of co-creation series of "self-portraits" album/playlist?

### Questions/Thoughts that Arise:
* Once they user is done, how much of that final song is played? Does it abruptly end when they close the window? Can we get it to fade out? Does it sustain for some determined length of time?
* The idea of an online repository to host the variety of songs is obviously is of interest. Soundcloud? Bandcamp? Website? 
* The musical possibilities will need to be wide here to make each track feel tailored to the user
* We can record out the audio to a wav file using [this strategy](https://forum.unity.com/threads/writing-audiolistener-getoutputdata-to-wav-problem.119295/), but is is also probably a good idea to note down the time/value of each decision in a text file so that the song can be recreated in higher quality later? 
* Is there still some sort of "theme"? Calling it "self-portrait song creator" is a bit yawn.

And it works!
