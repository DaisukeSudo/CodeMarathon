const main = input => (
  ({
    nodes,
    children,
    roots
  }) => (
    (({ appearanceOrder, completedOrder }) => (
      nodes.map(
        node => node.index
          + ' ' + appearanceOrder[node.index]
          + ' ' + completedOrder[node.index]
      ).join('\n')
    ))(
      [...Array(nodes.length + children.length).keys()]
        .reduce(
          ({ targetIndex, step, stack, appearanceOrder, completedOrder }) => (
            (
              node => (
                !!node
                  ? (
                    (
                      childIndex => (
                        childIndex
                          ? (stack.push(node) && ({ targetIndex: childIndex, step, stack, appearanceOrder, completedOrder }))
                          : ((completedOrder[node.index] = ++step) && ({ targetIndex: null, step, stack, appearanceOrder, completedOrder }))
                      )
                    )(
                      (appearanceOrder[node.index] || (appearanceOrder[node.index] = ++step))
                        && node.children.find(childIndex => !appearanceOrder[childIndex])
                    )
                  )
                  : ({ targetIndex, step, stack, appearanceOrder, completedOrder })
              )
            )
            ((targetIndex && nodes.find(node => node.index === targetIndex)) || stack.pop() || roots.shift())
          ),
          {
            targetIndex: null,
            step: 0,
            stack: [],
            appearanceOrder: {},
            completedOrder: {},
          }
        )
    )
  )
)(
  (
    nodes => (
      children => ({
        nodes,
        children,
        roots: (set => nodes.filter(node => !set.has(node.index)))(new Set(children)),
      })
    )(
      nodes.map(node => node.children).reduce((acc, x) => acc.concat(x), [])
    )
  )(
    input.trim().split('\n').slice(1)
      .map(s => s.split(' '))
      .map(line => ({ index: line[0], children: line.slice(2) }))
  )
);

console.log(main(require('fs').readFileSync('/dev/stdin', 'utf8')));

// const main = input => (({ nodes, children, roots }) => ((({ appearanceOrder, completedOrder }) => (nodes.map(node => node.index + ' ' + appearanceOrder[node.index] + ' ' + completedOrder[node.index]).join('\n')))([...Array(nodes.length + children.length).keys()].reduce(({ targetIndex, step, stack, appearanceOrder, completedOrder }) => ((node => (!!node ? ((childIndex => (childIndex ? (stack.push(node) && ({ targetIndex: childIndex, step, stack, appearanceOrder, completedOrder })) : ((completedOrder[node.index] = ++step) && ({ targetIndex: null, step, stack, appearanceOrder, completedOrder }))))((appearanceOrder[node.index] || (appearanceOrder[node.index] = ++step)) && node.children.find(childIndex => !appearanceOrder[childIndex]))) : ({ targetIndex, step, stack, appearanceOrder, completedOrder })))((targetIndex && nodes.find(node => node.index === targetIndex)) || stack.pop() || roots.shift())), { targetIndex: null, step: 0, stack: [], appearanceOrder: {}, completedOrder: {},}))))((nodes => (children => ({nodes, children, roots: (set => nodes.filter(node => !set.has(node.index)))(new Set(children))}))(nodes.map(node => node.children).reduce((acc, x) => acc.concat(x), [])))(input.trim().split('\n').slice(1).map(s => s.split(' ')).map(line => ({ index: line[0], children: line.slice(2) }))))
