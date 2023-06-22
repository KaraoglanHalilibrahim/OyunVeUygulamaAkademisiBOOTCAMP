Contact: aquageneral@hotmail.com

Adding a destructible wall into your game is now as simple as dragging and dropping. This pack contains both a standard "loose" brick wall, and also a "chunk" based brick wall. The loose brick wall is much like childrens wooden building blocks, in the way that they aren't connected. Where as, the chunk based wall is comprimised of multiple chunks of bricks, which can then be shattered into single bricks based on how much of a battering it recieves.

How to use:
After installing, navigate to the Prefab folder, and drag either the "ChunkWall", "ChunkOnlyWall" or the "StandardWall".

Brick Wall:
	This is a standard wall comprised of lots of loose bricks. There is no layered destruction.

Chunk Wall:
	A wall that is comprised of many "chunks" (typically they 4-6 bricks). The chunks can be broken further into multiple bricks. Chunks are broken by recieving damage from either hitting into an object, or by spinning very quickly. You can modify the strength of the chunks by changing the "impactMultiplier", or the "twistMultiplier" variable in the "DestructionManager" script. 

Chunk Only Wall:
	This wall in the same as the chunk wall (where it is comprised of multiple chunks), however the chunks are indestructible. This is ideal for scalability/performance.

Recommendations:
- "Min penetration for penalty" inside of Edit>Project Settings>Physics should be lowered to a value such as 0.005. The reason being is that since the physics engine is essentially tolerating 1cm (0.01m) of interpentration, 10 rows of bricks will total 10cm of total tolerance. That isn't necessarily always the case, but it is more noticable when objects are stacked.

Easy Destructible Wall - Made by Jesse Stiller