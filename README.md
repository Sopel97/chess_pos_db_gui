# chess_pos_db_gui

# Preview:
![](https://raw.githubusercontent.com/Sopel97/chess_pos_db_gui/master/assets/graphics/example.png)

GUI created for the project [chess_pos_db](https://github.com/Sopel97/chess_pos_db).
It requires chess_pos_db.exe to be present in the same directory.
A chess_pos_db.exe process is spawned by the gui (previously ensuring that it's not already present by killing all of its instances) and a TCP connection in made through which the requests are made.

The gui is in a rough, but usable state. Codebase is at a prototype-like stage. Don't expect clean code.

Currently the biggest issue is no good chess abstraction in C#. Going by Chess DOT Net's limitations is taking a tall on the codebase. It is a priority to either move to a better library or create one.

# Features
- Input chess board with FEN field and navigable move history
- Excel-like data display with easy selection and copying through CTRL+C
- Creation of position databases from a list of PGN files optionally partitioned by game level (human/engine/server).
- (Automatic) querying of position data from a loaded database
- Filtering of queried data:

    - game level (human, engine, server)
    - continuations and/or transpositions

- Evaluation data provided by (https://chessdb.cn/queryc_en/), building up on a massive effort by Bojun Guo (noobpwnftw)
- A tool for dumping all FENs for all positions with at least a given number of instances into an EPD file.

# Dependencies
- Chess DOT Net

# Installation
Put the compiled artifacts and the assets folder (or unzip the released binaries) into a directory with chess_pos_db.exe.