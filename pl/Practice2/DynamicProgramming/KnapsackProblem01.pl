% http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=DPL_1_B

:- use_module(library(clpfd)).

read_string(Str) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, Str).

read_number(Num) :-
  read_string(Str),
  number_string(Num, Str).

read_numbers2(0, [], []).
read_numbers2(N, [X|Xs], [Y|Ys]) :-
  read_number(X),
  read_number(Y),
  N1 is N - 1,
  read_numbers2(N1, Xs, Ys).

bs(0, []).
bs(N, [B|Bs]) :-
  N1 is N - 1,
  bs(N1, Bs).

solve(N, W, Vs, Ws, V) :-
  bs(N, Bs),
  Bs ins 0..1,
  scalar_product(Ws, Bs, #=<, W),
  scalar_product(Vs, Bs, #=, V),
  labeling([max(V)], [V|Bs]).

main :-
  read_number(N),
  read_number(W),
  read_numbers2(N, Vs, Ws),
  solve(N, W, Vs, Ws, Ret),
  writeln(Ret).

:- main.

% ref: http://www.nct9.ne.jp/m_hiroi/prolog/clp02.html
