## 2024-05-15 - Vector3 Distance Optimization
**Learning:** Legacy XNA/MonoGame projects often perform large numbers of `Vector3.Distance` calculations in particle systems (like `debrisBlood`). These can cause performance bottlenecks due to the expensive square root operations involved, especially in tight loops.
**Action:** When working on XNA/MonoGame projects, proactively look for opportunities to replace `Vector3.Distance(a, b) < c` with `Vector3.DistanceSquared(a, b) < c * c` in performance-critical code paths.
## 2024-05-18 - Replacing Vector3.Distance with Vector3.DistanceSquared
**Learning:** Found an opportunity to replace `Vector3.Distance` with `Vector3.DistanceSquared` inside heavily used collision loops (`Princess.cs`, `Princess4.cs`). This prevents a slow square root calculation per entity per frame.
**Action:** When working in games, watch for `Vector3.Distance` calls compared against thresholds in `Update` loops and replace with `DistanceSquared` against squared thresholds.
