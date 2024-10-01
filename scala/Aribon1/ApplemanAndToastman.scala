// https://codeforces.com/contest/462/problem/C

object Main {

  def solve(a: Array[Long]) =
    a.foldLeft((a.sum, a.sum)) { case ((score, total), x) => (score + total, total - x) }._1 - a.last

  def main(args: Array[String]) = {
    val _ = io.StdIn.readLine
    val a = io.StdIn.readLine.split(" ").map(_.toLong).sorted
    println(solve(a))
  }
}

// https://codeforces.com/contest/462/submission/283878843
