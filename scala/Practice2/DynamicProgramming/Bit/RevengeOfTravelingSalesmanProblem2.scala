// https://atcoder.jp/contests/s8pc-1/tasks/s8pc_1_g

object Main {

  def main(args: Array[String]) = {
    val readLine = () => io.StdIn.readLine.split(' ')
    val (n, m) = ((arr: Array[Int]) => (arr(0), arr(1)))(readLine().map(_.toInt))
    val graph = List.range(0, m)
      .map(_x => readLine())
      .map(arr => (arr(0).toInt - 1, arr(1).toInt - 1, arr(2).toLong, arr(3).toLong))
      .map { case (s, t, d, time) => if (s < t) (s, t, d, time) else (t, s, d, time) }
    val memV = Array.range(0, 1 << n).map(_x => Array.fill[Long](n)(Long.MaxValue))
    val memC = Array.range(0, 1 << n).map(_x => Array.fill[Long](n)(0L))
    memV(1)(0) = 0L
    memC(1)(0) = 1L

    val fn = (i: Int, j: Int, i2: Int, j2: Int, d: Long, time: Long) => {
      val preV = memV(i2)(j2)
      if (preV != Long.MaxValue) {
        val curV = preV + d
        if (curV <= time) {
          val accV = memV(i)(j)
          val accC = memC(i)(j)
          val preC = memC(i2)(j2)
          val (v, c) = i match {
            case _x if curV < accV  => (curV, preC)
            case _x if curV == accV => (accV, accC + preC)
            case _x => (accV, accC)
          }
          memV(i)(j) = v
          memC(i)(j) = c
        }
      }
    }

    for (
      vx <- 1 until (1 << n) by 2;
      (s, t, d, time) <- graph if (vx >> s & 1) == 1 && (vx >> t & 1) == 1;
      (i, j, i2, j2) <- Stream(
        (vx, t, vx ^ (1 << t), s),
        (vx, s, vx ^ (1 << s), t)
      )
    ) {
      fn(i, j, i2, j2, d, time)
    }

    for (
      (s, t, d, time) <- graph if s == 0;
      (i, j, i2, j2) <- Stream(
        (0, 0, (1 << n) - 1, t)
      )
    ) {
      fn(i, j, i2, j2, d, time)
    }

    val v = memV(0)(0)
    val c = memC(0)(0)

    if (c == 0L)
      println("IMPOSSIBLE")
    else
      println(s"${v} ${c}")
  }
}

// https://atcoder.jp/contests/s8pc-1/submissions/20682057
