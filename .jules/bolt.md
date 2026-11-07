## 2024-05-15 - Vector3 Distance Optimization
**Learning:** Legacy XNA/MonoGame projects often perform large numbers of `Vector3.Distance` calculations in particle systems (like `debrisBlood`). These can cause performance bottlenecks due to the expensive square root operations involved, especially in tight loops.
**Action:** When working on XNA/MonoGame projects, proactively look for opportunities to replace `Vector3.Distance(a, b) < c` with `Vector3.DistanceSquared(a, b) < c * c` in performance-critical code paths.
## 2024-05-18 - Replacing Vector3.Distance with Vector3.DistanceSquared
**Learning:** Found an opportunity to replace `Vector3.Distance` with `Vector3.DistanceSquared` inside heavily used collision loops (`Princess.cs`, `Princess4.cs`). This prevents a slow square root calculation per entity per frame.
**Action:** When working in games, watch for `Vector3.Distance` calls compared against thresholds in `Update` loops and replace with `DistanceSquared` against squared thresholds.
## 2024-05-24 - Avoid Math.Sqrt by using DistanceSquared for constant distance comparisons
**Learning:** Checking distances using `Vector3.Distance(a, b) < limit` invokes `Math.Sqrt` which is computationally expensive and unnecessary when compared against a literal threshold.
**Action:** Always prefer `Vector3.DistanceSquared(a, b) < (limit * limit)` for comparing distances to thresholds to avoid calculating square roots, particularly in hot code paths or loops.
## 2024-05-24 - Avoiding Math.Sqrt for threshold comparisons in teleport logic
**Learning:** Found an opportunity to replace `Vector3.Distance` with `Vector3.DistanceSquared` inside jump and teleport logic (`BloodnBacon.cs`, `BloodnBacon4PT.cs`). This prevents a slow square root calculation per frame whenever jumping while a teleport is available.
**Action:** When working in games, watch for `Vector3.Distance` calls compared against thresholds and replace with `DistanceSquared` against squared thresholds.
## 2024-05-24 - Avoiding Math.Sqrt for Length comparisons
**Learning:** Legacy XNA/MonoGame projects perform many `Vector3.Length()` calculations in update loops (like physics and collision in `skullDupe.cs`). These involve expensive square root operations, which become bottlenecks in tight loops.
**Action:** Always replace `Vector3.Length() < limit` (and similar inequalities) with `Vector3.LengthSquared() < limit * limit` to avoid calculating square roots unnecessarily.

## 2024-05-20 - Finding Closest Points Optimization
**Learning:** In legacy games, searching for a closest point inside a loop (like finding nearest seat in `astro.cs`) using `Vector3.Distance` introduces multiple expensive `Math.Sqrt` calls unnecessarily. A threshold calculation tracking the shortest distance doesn't need to know the true distance.
**Action:** When tracking minimum distance in a loop, initialize the tracking variable to the squared initial value (e.g. `100000f * 100000f`) and track `DistanceSquared` instead of `Distance`.

## 2024-05-19 - Replacing Vector2/Vector3.Length with LengthSquared
**Learning:** Checking distances using `Vector.Length() > limit` invokes `Math.Sqrt` which is computationally expensive and unnecessary when compared against a literal threshold. We found numerous instances where `vector.Length() > 50f` was checked in `Update` loops related to Princess/Cutty bosses in BloodandBacon.
**Action:** When working in games, watch for `Vector.Length()` calls compared against thresholds and replace with `LengthSquared()` against squared thresholds (e.g. `vector.LengthSquared() > 2500f`).

## 2024-05-18 - Avoid Vector3.Length()
**Learning:** Checking vector magnitudes using `Vector3.Length() < limit` invokes `Math.Sqrt` which is computationally expensive and unnecessary when compared against a literal threshold. This is a common performance bottleneck in MonoGame and XNA applications.
**Action:** When working on XNA/MonoGame projects, proactively look for opportunities to replace `Vector3.Length() < c` with `Vector3.LengthSquared() < c * c` in performance-critical code paths.

## 2024-05-24 - Avoiding Math.Sqrt for distance logic in twinBones
**Learning:** Found an opportunity to replace `Vector3.Distance` with `Vector3.DistanceSquared` inside `twinBones` logic (`BloodnBacon.cs`). This prevents a slow square root calculation per frame whenever evaluating the distance to the twin.
**Action:** When working in games, watch for `Vector3.Distance` calls compared against thresholds and replace with `DistanceSquared` against squared thresholds.
## 2024-05-18 - Caching Length for further optimizations
**Learning:** Sometimes, variables have a Length or Distance checked if it's strictly > 0, but then the Length itself is calculated again immediately after if it is greater than zero. Replacing Length() with LengthSquared() doesn't completely avoid the square root if it's called immediately afterwards! But extracting it fully into a local variable might be slightly beyond a safe automated script unless carefully monitored.
**Action:** When replacing Length() < c, always check what happens *inside* the block. If Length() is used again, refactoring to calculate and cache it locally might be more beneficial, or at least ensuring the automated script handles this edge case.

## 2024-11-07 - Avoid Vector3.Length in Update Loops
**Learning:** Checking vector magnitudes using `Vector3.Length() > limit` invokes `Math.Sqrt` which is computationally expensive and unnecessary when compared against a literal threshold. This is a common performance bottleneck in MonoGame and XNA applications.
**Action:** When working on XNA/MonoGame projects, proactively look for opportunities to replace `Vector3.Length() > c` with `Vector3.LengthSquared() > c * c` in performance-critical code paths.
