from copy import deepcopy

puzzle = [
    [None, None,    6, None, None,    5, None, None, None],
    [8,    None, None, None, None, None, None,    4,    9],
    [None,    7,    3, None,    9, None,    2, None, None],
    [   5,    6, None, None, None,    3, None, None, None],
    [None, None,    2,    6, None, None, None, None, None],
    [   1,    9,    7, None, None, None,    6, None,    3],
    [None, None, None, None,    6, None, None,    9, None],
    [   6, None,    5, None, None,    9,    1, None, None],
    [None, None, None,    5,    3, None, None, None,    4],
]


def is_valid(puzzle):
    """Given a partial 9x9 Sudoku puzzle, determine if current state is valid."""
    for i in range(0, 9):
        # Row
        if not is_section_valid(puzzle[i]):
            return False

        # Column
        column_values = []
        for j in range(0, 9):
            column_values.append(puzzle[j][i])
        if not is_section_valid(column_values):
            return False

        # Square
        # (0, 0), (0, 3), (0, 6)
        # (3, 0), (3, 3), (3, 6)
        # (6, 0), (6, 3), (6, 6)
        x = i // 3 * 3
        y = i  % 3 * 3
        square_values = []
        for m in range(0, 3):
            for n in range(0, 3):
                square_values.append(puzzle[x + m][y + n])
        if not is_section_valid(square_values):
            return False

    return True


def is_section_valid(row):
    """Given 9 values (from row, column, or square), determine if valid."""
    last = None
    for v in sorted([x for x in row if x is not None]):
        if last == v:
            # Same number
            return False
        last = v
    return True



def gen_next_states(puzzle):
    next_coord = find_next_empty_coord(puzzle)
    if next_coord is None:
        return None
    (x, y) = next_coord

    puzzles = []
    for i in range(0, 9):
        current = deepcopy(puzzle)
        current[x][y] = i + 1
        if is_valid(current):
            puzzles.append(current)

    return puzzles


def find_next_empty_coord(puzzle) -> tuple[int, int]:
    for i in range(0, 9):
        for j in range(0, 9):
            if puzzle[i][j] is None:
                return (i, j)
    return None


def dfs(puzzle):
    state = [puzzle]

    while True:
        puzzle = state.pop(0)
        puzzles = gen_next_states(puzzle)

        # Answer, or stuck
        if puzzles is None:
            return puzzle

        # Add to stack
        state = puzzles + state


print(is_valid(puzzle))
answer = dfs(puzzle)
print(answer)
print(is_valid(answer))
