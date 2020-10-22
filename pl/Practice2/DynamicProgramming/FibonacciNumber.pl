% http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=ALDS1_10_A

read_string(Str) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, Str).

read_number(Num) :-
  read_string(Str),
  number_string(Num, Str).

fib(Ret, _, 0, Ret).
fib(P1, P2, I, Ret) :-
  N1 is P1 + P2,
  N2 is P1,
  I1 is I - 1,
  fib(N1, N2, I1, Ret).

solve(N, Ret) :-
  N >= 0,
  fib(1, 0, N, Ret).

main :-
  read_number(N),
  solve(N, Ret),
  writeln(Ret).

:- main.
