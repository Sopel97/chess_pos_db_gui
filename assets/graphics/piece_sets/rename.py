import re
import bz2
import sys
import time
from os import listdir
from os import walk
import os
from os.path import isfile, join

path = '.'
pgns = [f for f in listdir(path) if isfile(join(path, f)) and f.endswith('.png')]

m = {
    'b' : 'black_',
    'w' : 'white_',
    'B' : 'bishop',
    'N' : 'knight',
    'R' : 'rook',
    'P' : 'pawn',
    'K' : 'king',
    'Q' : 'queen'
}

for pgn in pgns:
    parts = pgn.split('.')
    os.rename(pgn, m[parts[0][0]] + m[parts[0][1]] + '.' + parts[1])
