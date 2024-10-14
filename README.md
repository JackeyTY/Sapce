# Space

![space](img/space.gif)

An endless runner game inspired by Space Invaders.

**Features:**

* Player uses finger to move the spaceship on screen.
* Player shoots down enemy spacecrafts to obtain energy, which is showcased by the energy bar on the right. When the energy bar is full, player acquires a one-time missile to destroy all enemy spacecrafts on screen at time of launch. Missile is launched when player presses the missile button, after which energy bar is set back to 0.
* Distance travelled (according to play time) and the highest distance travelled are recorded and displayed. Highest distance travelled is stored on disk.
* Player spaceship can now fire multiple bullets at the same time.
* Airdrop system for additional resources. Three types of airdrops including: heart to increase one life; bullet to increase bullet number or bullet damage; power up to increase firing frequency. Airdrops are instantiated randomly.
* Player spaceship, enemy spacecrafts, bullets, airdrops all have collider and rigid body components. When enemy spacecrafts are shot down, they will spiral down in a random direction and create an explosion particle system effect. 
* Game difficulty increases with time, which affects enemy spacecrafts spawn period, spawn number, life, bullet damage, increases airdrop system max spawn period, increase max energy level.
* Background scene to present the background video and background music, which is also loaded throughout the game. Other scenes are loaded additively.
* All events have according sound effects, including: player spaceship bullet fire, enemy spacecraft bullet fire, bullet strike, enemy spacecraft shot down explosion, heart airdrop acquired, bullet airdrop acquired, power up airdrop acquired.