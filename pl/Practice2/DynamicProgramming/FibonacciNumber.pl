% http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=ALDS1_10_A

read_string(Str) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, Str).

read_number(Num) :-
  read_string(Str),
  number_string(Num, Str).

fib(P1, P2, 0, Ret) :-
  Ret is P1.
fib(P1, P2, I, Ret) :-
  I > 0,
  N1 is P1 + P2,
  N2 is P1,
  NI is I - 1,
  fib(N1, N2, NI, Ret).

solve(N, Ret) :-
  fib(1, 0, N, Ret).

main :-
  read_number(N),
  solve(N, Ret),
  writeln(Ret).

:- main.
