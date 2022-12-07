from numpy import Inf

grid =[]

with open("15.txt", "r") as file:
    for line in file.readlines():        
        grid.append([int(x) for x in line.strip()])

class Position:
    def __init__(self, row: int, col: int, risk: int):
        self.distance = Inf
        self.risk = risk
        self.row = row
        self.col = col

def is_neighbor(pos1: Position, pos2: Position):
    row_diff = abs(pos1.row - pos2.row)
    col_diff = abs(pos1.col - pos2.col)
    return (row_diff + col_diff) == 1

positions = []
unvisited = []
visited = []

for i in range(len(grid)):
    positions.append([])
    for j in range(len(grid[0])):
        newPos = Position(i, j, grid[i][j])
        positions[i].append(newPos)
        unvisited.append(newPos)

current = positions[0][0]
current.distance = 0

while(True):
    unvisited = [x for x in unvisited if not(x.row == current.row and x.col == current.col)]
    neighbors = [x for x in visited + unvisited if is_neighbor(current, x)]
    for neighbor in neighbors:
        if current.distance + neighbor.risk < neighbor.distance:
            neighbor.distance = current.distance + neighbor.risk
    visited.append(current)
    if(current.row == len(grid) - 1 and current.col == len(grid) - 1):
        break
    unvisited.sort(key=lambda x: x.distance)
    current = unvisited[0]

target = [x for x in visited if x.row = 99 and x.col = 99]

print()
