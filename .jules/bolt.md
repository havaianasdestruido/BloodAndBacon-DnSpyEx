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
