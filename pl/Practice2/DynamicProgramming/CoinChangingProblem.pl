% http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=DPL_1_A

:- use_module(library(clpfd)).

read_string(Str) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, Str).

read_number(Num) :-
  read_string(Str),
  number_string(Num, Str).

read_numbers(0, []).
read_numbers(N, [X|Xs]) :-
  read_number(X),
  N1 is N - 1,
  read_numbers(N1, Xs).

bs(0, []).
bs(N, [B|Bs]) :-
  N1 is N - 1,
  bs(N1, Bs).

solve(N, M, Cs, V) :-
  bs(M, Bs),
  maplist(#=<(0), Bs),
  scalar_product(Cs, Bs, #=, N),
  sum(Bs, #=, V),
  labeling([min(V)], [V|Bs]).

main :-
  read_number(N),
  read_number(M),
  read_numbers(M, Cs),
  solve(N, M, Cs, Ret),
  writeln(Ret).

:- main.
