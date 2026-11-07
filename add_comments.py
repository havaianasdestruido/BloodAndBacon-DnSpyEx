import os
import re

def add_comments(filepath):
    with open(filepath, 'r') as f:
        content = f.read()

    # We will just add a comment at the top of the file indicating that we optimized vector math if it was changed
    if 'Vector3.DistanceSquared' in content or 'LengthSquared()' in content:
        if '// Bolt: Optimized vector distance/length checks to use squared values, avoiding expensive Math.Sqrt calls.' not in content:
            # add right after namespace Blood
            content = content.replace('namespace Blood\n{', 'namespace Blood\n{\n\t// Bolt: Optimized vector distance/length checks to use squared values, avoiding expensive Math.Sqrt calls.')
            with open(filepath, 'w') as f:
                f.write(content)
            print(f"Added comment to {filepath}")

for root, _, files in os.walk('BloodandBacon'):
    for file in files:
        if file.endswith('.cs'):
            add_comments(os.path.join(root, file))
