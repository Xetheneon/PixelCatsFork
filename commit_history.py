import subprocess
from html import escape
from datetime import datetime

command = [
    "git",
    "log",
    "--all",
    "--graph",
    "--decorate",
    "--oneline",
    "--date=short",
    "--pretty=format:%C(auto)%h%Creset %C(bold)%s%Creset %C(dim white)%an%Creset %C(cyan)%d%Creset"
]

result = subprocess.run(
    command,
    capture_output=True,
    text=True,
    encoding="utf-8",
    errors="replace"
)

git_graph = result.stdout

html = f"""
<!DOCTYPE html>
<html>
<head>
<meta charset="UTF-8">
<title>Git Branch History</title>
<style>
body {{
    margin: 0;
    background: #020617;
    color: #e5e7eb;
    font-family: Arial, sans-serif;
}}

.page {{
    padding: 40px;
}}

.header {{
    margin-bottom: 28px;
}}

h1 {{
    margin: 0;
    font-size: 34px;
}}

.subtitle {{
    color: #94a3b8;
    margin-top: 8px;
}}

.graph-box {{
    background: #050816;
    border: 1px solid #1e293b;
    border-radius: 14px;
    padding: 24px;
    overflow-x: auto;
}}

pre {{
    font-family: Consolas, "Courier New", monospace;
    font-size: 14px;
    line-height: 1.55;
    white-space: pre;
    margin: 0;
}}

.footer {{
    margin-top: 24px;
    color: #64748b;
    font-size: 12px;
}}
</style>
</head>
<body>
<div class="page">
    <div class="header">
        <h1>Git Branch History</h1>
        <div class="subtitle">Visual commit graph generated from all branches</div>
    </div>

    <div class="graph-box">
        <pre>{escape(git_graph)}</pre>
    </div>

    <div class="footer">
        Generated on {datetime.now().strftime("%d %b %Y, %H:%M")}
    </div>
</div>
</body>
</html>
"""

with open("branch_history.html", "w", encoding="utf-8") as f:
    f.write(html)

print("Done. Created branch_history.html")