using cs_games.api;

API.ConnectToDB("fha_cs", "F2j77*D%@4H5e$P3WuX^JX#27$k9aeAr");
API.PrintJsonArray(
    API.SQLQuery(
        $"SELECT match_history.ID AS ID, spieler.Name AS Name FROM match_history JOIN spieler ON match_history.sieger = spieler.ID WHERE Typ = \'Dame\' ORDER BY match_history.ID DESC",
        "Keine Historie vorhanden"
    ),
    new List<string>{"ID"}
);