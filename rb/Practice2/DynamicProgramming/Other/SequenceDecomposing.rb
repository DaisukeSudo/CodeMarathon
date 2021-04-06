# https://atcoder.jp/contests/abc134/tasks/abc134_e

class Object
  def let(f)
    f.call self
  end
end

gets
  .to_i
  .let(->(n) {
    Array.new(n + 1, 10 ** 9 + 1).let(->(memo) {
      (1..n)
        .map { |_| gets.to_i }
        .reduce(0) { |len, x|
          memo[len] >= x ? (
            (memo[len + 1] = x)
              .let(->(_) { len + 1 })
          ) : (
            (1..17)
              .reduce([1, len]) { |(li, ri), _|
                (li + (ri - li) / 2).let(->(mi) {
                  memo[mi] < x ? [li, mi] : [mi + 1, ri]
                })
              }
              .let(->((i, _)) { memo[i] = x })
              .let(->(_) { len })
          )
        }
    })
  })
  .let(method(:puts))

# https://atcoder.jp/contests/abc134/submissions/21543580
