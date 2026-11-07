1. **Optimize Vector Distance and Length checks**
   - Use a python script to scan the `BloodandBacon/` directory for C# files.
   - Specifically replace instances of `Vector3.Distance(a, b) < c` with `Vector3.DistanceSquared(a, b) < c * c`.
   - Also replace instances of `Vector3.Length() < c` with `Vector3.LengthSquared() < c * c`.
   - These optimizations avoid computationally expensive `Math.Sqrt` calls in performance-critical MonoGame/XNA code paths.

2. **Verify syntax**
   - Run a custom python script (`check_braces.py`) to verify balanced braces and parentheses since `dotnet build` is not available for this legacy project.

3. **Complete pre-commit steps to ensure proper testing, verification, review, and reflection are done.**

4. **Submit PR**
   - Submit the PR with the title '⚡ Bolt: [performance improvement]' and description detailing the optimization and its impact.
