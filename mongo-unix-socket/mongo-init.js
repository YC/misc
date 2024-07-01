db.createUser(
    {
        user: "test",
        pwd: "test",
        roles: [
            {
                role: "readWrite",
                db: "test"
            }
        ]
    }
);

db = db.getSiblingDB('test');
db.test.insert({ test: 'test' });
