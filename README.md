# chess_pos_db_gui

# Preview:
![](https://raw.githubusercontent.com/Sopel97/chess_pos_db_gui/master/docs/img/main.png)
![](https://raw.githubusercontent.com/Sopel97/chess_pos_db_gui/master/docs/img/creation.png)
![](https://raw.githubusercontent.com/Sopel97/chess_pos_db_gui/master/docs/img/merging.png)

GUI created for the project [chess_pos_db](https://github.com/Sopel97/chess_pos_db).
It requires chess_pos_db.exe to be present in the same directory.
A chess_pos_db.exe process is spawned by the gui (previously ensuring that it's not already present by killing all of its instances) and a TCP connection in made through which the requests are made.

# Features
- Chess board display

    - draggable pieces
    - move history
    - easy way to set a position from FEN
    - easy way to set whole move history from PGN
    - board flipping, resetting
    - copy FEN
    - board and piece themes

- Excel-like data display

    - (Automatic) querying of position data from a loaded database
    - empirical WDL, perf%, draw%, human%
    - average elo difference between players for each position (if available) and adjusted performance based on expected performance
    - Filtering of queried data:

        - by game level (human, engine, server) (based on partitioned PGNs during the database creation)
        - continuations and/or transpositions

    - Evaluation data (cp and perf%) provided by (https://chessdb.cn/queryc_en/), building up on a massive effort by Bojun Guo (noobpwnftw)
    - Display for the data about the first game with the given position.
    - Statistically sound move quality calculation

        - based on empirical performance, confidence (based on frequency), expected performance based on elo differences, and engine evaluation from chessdb.cn.
        - configurable weights
        - optional normalization

- A tool for dumping all FENs for all positions with at least a given number of instances into an EPD file.

- Configurable local engine analysis.

- User friendly GUIs for creation and management of position databases (with chess_pos_db backend).

# Dependencies
- Chess DOT Net
- chess_pos_db binaries

# Installation
Put the compiled artifacts and the assets folder (or unzip the released binaries) into a directory with chess_pos_db.exe.