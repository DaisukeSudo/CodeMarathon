# https://atcoder.jp/contests/abs/tasks/arc089_a

class Object
  def let(f)
    f.call self
  end
end

gets
  .to_i
  .let(->(n) { (1..n) })
  .map { |_| gets.split(" ").map(&:to_i) }
  .reduce([0, 0, 0]) { |p, n|
    p &&
      [p, n].let(->(((t1, x1, y1), (t2, x2, y2))) {
        (t2 & 1) === ((x2 + y2) & 1) &&
          ((t2 - t1) >= ((x2 - x1).abs + (y2 - y1).abs))
      }) &&
      n
  }
  .let(->(x) { x ? "Yes" : "No" })
  .let(method(:puts))

# https://atcoder.jp/contests/abs/submissions/21525732
