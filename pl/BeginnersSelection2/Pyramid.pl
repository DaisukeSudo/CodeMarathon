% https://atcoder.jp/contests/abc112/tasks/abc112_c

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

read_numbers2(0, _, []).
read_numbers2(H, W, [Xs|Xss]) :-
  read_numbers(W, Xs),
  H1 is H - 1,
  read_numbers2(H1, W, Xss).

height(X1, Y1, Hi, H) :-
  Hi =:= 0,
  H =< X1 + Y1.
height(X1, Y1, Hi, H) :-
  Hi > 0,
  H is X1 + Y1 + Hi.

search([], _, _, _) :- !.
search([[Xi, Yi, Hi]|XYHs], Cx, Cy, H) :-
  abs(Xi - Cx, X1),
  abs(Yi - Cy, Y1),
  height(X1, Y1, Hi, H),
  search(XYHs, Cx, Cy, H).

format_result([(Cx, Cy, H)], Ret) :-
  atomics_to_string([Cx, ' ', Cy, ' ', H], Ret).

solve(XYHs, Ret) :-
  between(0, 100, Cx),
  between(0, 100, Cy),
  bagof((Cx, Cy, H), search(XYHs, Cx, Cy, H), Hits),
  format_result(Hits, Ret).

main :-
  read_number(N),
  read_numbers2(N, 3, XYHs),
  solve(XYHs, Ret),
  write(Ret).

:- main.

% https://atcoder.jp/contests/abc112/submissions/17410708 (RE)
