f = open('input', 'r')

# Read numbers
numbers = {}
for line in f.readlines():
    if line.strip() == '':
        continue
    numbers[int(line.strip())] = True

for number in numbers.keys():
    if 2020 - number  in numbers:
        print(number * (2020 - number))
        break
