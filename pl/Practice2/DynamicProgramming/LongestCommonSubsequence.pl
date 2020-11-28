% http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=ALDS1_10_C

:- use_module(library(clpfd)).

read_string(Str) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, Str).

read_number(Num) :-
  read_string(Str),
  number_string(Num, Str).

read_strings_2(0, []).
read_strings_2(N, [(X,Y)|XYs]) :-
  read_string(X),
  read_string(Y),
  N1 is N - 1,
  read_strings_2(N1, XYs).

match_item(_, _, M0, M1, M1, _, false) :- M1 < M2, !.
match_item(XY, XY, M0, M1, M2, false, true) :- M2 is M0 + 1, !.
match_item(_, _, M, M, M, _, false) :- !.
match_item(_, _, M, _, M, U, U) :- !.

match_line(_, [], _, _, [], _).
match_line(X, [Y|Ys], M0, [M1|Ms], [M2|Ms2], U1) :-
  match_item(X, Y, M0, M1, M2, U1, U2),
  match_line(X, Ys, M2, Ms, Ms2, U2).
match_line(X, Ys, [M0|Ms], Ms2) :-
  match_line(X, Ys, M0, Ms, Ms2, false).

match_lines([], _, Ret, Ret).
match_lines([X|Xs], Ys, Ms, Ret) :-
  match_line(X, Ys, Ms, Ms2),
  match_lines(Xs, Ys, Ms2, Ret).

array(0, _, []).
array(N, X, [X|Xs]) :-
  N1 is N - 1,
  array(N1, X, Xs).

solve(Xstr, Ystr, Ret) :-
  string_chars(Xstr, Xs),
  string_chars(Ystr, Ys),
  length(Ys, L),
  L1 is L + 1,
  array(L1, 0, Ms),
  match_lines(Xs, Ys, Ms, Ms2),
  last(Ms2, Ret).

solves([], []).
solves([(X, Y)|XYs], [R|Rs]) :-
  solve(X, Y, R),
  solves(XYs, Rs).

writelns([]).
writelns([T|Ts]) :-
  writeln(T),
  writelns(Ts).

main :-
  read_number(N),
  read_strings_2(N, XYs),
  solves(XYs, Rets),
  writelns(Rets).

:- main.
