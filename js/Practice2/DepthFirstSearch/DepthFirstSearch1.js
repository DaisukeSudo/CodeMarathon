// http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=ALDS1_11_B

const main = input => {
  const nodes = input.trim().split('\n').slice(1)
    .map(s => s.split(' '))
    .map(line => ({ index: line[0], children: line.slice(2) }));
  const children = nodes.map(node => node.children).reduce((acc, x) => acc.concat(x), []);
  const roots = (set => nodes.filter(node => !set.has(node.index)))(new Set(children));
  const loopCount = nodes.length + children.length;
  const { appearanceOrder, completedOrder } =
    [...Array(loopCount).keys()]
      .reduce(
        acc => {
          const {
            targetIndex,
            step,
            stack,
            appearanceOrder,
            completedOrder,
          } = acc;
          const node = (
            targetIndex && nodes.find(node => node.index === targetIndex)
          ) || stack.pop() || roots.shift();
          if (!node) return acc;
          let newStep = step;
          if (!appearanceOrder[node.index]) {
            appearanceOrder[node.index] = ++newStep;
          }
          const childIndex = node.children.find(childIndex => !appearanceOrder[childIndex]);
          if (childIndex) {
            stack.push(node);
            return { ...acc, step: newStep, targetIndex: childIndex };
          } else {
            completedOrder[node.index] = ++newStep;
            return { ...acc, step: newStep, targetIndex: null };
          }
        },
        {
          targetIndex: null,
          step: 0,
          stack: [],
          appearanceOrder: {},
          completedOrder: {},
        }
      );
    return nodes.map(
      node => node.index
        + ' ' + appearanceOrder[node.index]
        + ' ' + completedOrder[node.index]
    ).join('\n');
};

console.log(main(require('fs').readFileSync('/dev/stdin', 'utf8')));

// let inputBuf = '';
// process.stdin.resume();
// process.stdin.setEncoding('utf8');
// process.stdin.on('data', (chunk) => inputBuf += chunk);
// process.stdin.on('end', () => console.log(main(inputBuf)));
