## 2024-05-15 - Vector3 Distance Optimization
**Learning:** Legacy XNA/MonoGame projects often perform large numbers of `Vector3.Distance` calculations in particle systems (like `debrisBlood`). These can cause performance bottlenecks due to the expensive square root operations involved, especially in tight loops.
**Action:** When working on XNA/MonoGame projects, proactively look for opportunities to replace `Vector3.Distance(a, b) < c` with `Vector3.DistanceSquared(a, b) < c * c` in performance-critical code paths.
