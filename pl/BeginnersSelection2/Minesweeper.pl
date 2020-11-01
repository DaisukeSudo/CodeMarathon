% https://atcoder.jp/contests/abc075/tasks/abc075_b

read_string(Str) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, Str).

read_number(Num) :-
  read_string(Str),
  number_string(Num, Str).

read_char(Char) :-
  current_input(Input),
  read_string(Input, 1, Char).

read_chars(0, []).
read_chars(N, [X|Xs]) :-
  read_char(X),
  N1 is N - 1,
  read_chars(N1, Xs).

read_chars2(0, _, []).
read_chars2(H, W, [Xs|Xss]) :-
  read_chars(W, Xs),
  read_char(N),
  H1 is H - 1,
  read_chars2(H1, W, Xss).

discover(I, J, G) :-
  I1 is I - 1,
  I2 is I + 1,
  J1 is J - 1,
  J2 is J + 1,
  between(I1, I2, Ix),
  between(J1, J2, Jx),
  nth0(Ix, G, L),
  nth0(Jx, L, M),
  M =:= "#".

replace_item([], _, _, _, []).
replace_item(["#"|Ss], I, J, G, ["#"|Rs]) :-
  J1 is J + 1,
  replace_item(Ss, I, J1, G, Rs).
replace_item(["."|Ss], I, J, G, [R|Rs]) :-
  findall(_, discover(I, J, G), List),
  length(List, R),
  J1 is J + 1,
  replace_item(Ss, I, J1, G, Rs).

replace_line([], _, _, []).
replace_line([Ss|Sss], I, G, [Rs|Rss]) :-
  replace_item(Ss, I, 0, G, Rs),
  I1 is I + 1,
  replace_line(Sss, I1, G, Rss).

solve(Sss, Ret) :-
  replace_line(Sss, 0, Sss, Ret).

write_line([]).
write_line([S|Ss]) :-
  write(S),
  write_line(Ss).

write_lines([]).
write_lines([Ss|Sss]) :-
  write_line(Ss),
  writeln(""),
  write_lines(Sss).

main :-
  read_number(H),
  read_number(W),
  read_chars2(H, W, Sss),
  solve(Sss, Ret),
  write_lines(Ret).

:- main.

% https://atcoder.jp/contests/abc075/submissions/17770266
