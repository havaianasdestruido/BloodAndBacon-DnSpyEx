## $(date +%Y-%m-%d) - Optimize Vector3.Distance
**Learning:** Math.Sqrt is a relatively expensive operation, and it's called under the hood in `Vector3.Distance`. XNA/Monogame provides `Vector3.DistanceSquared` which avoids the square root and is faster. For many comparisons against a constant threshold (`Vector3.Distance(a, b) < 50f`), we can square the threshold instead (`Vector3.DistanceSquared(a, b) < 2500f`).
**Action:** Replace `Vector3.Distance` with `Vector3.DistanceSquared` where we are comparing to constant floats across game logic.
