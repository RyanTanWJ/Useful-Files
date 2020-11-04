import * as ecsy from 'ecsy';
// console.log(ecsy);

function myFunction() {
    var world = new ecsy.World();

    class Health extends ecsy.Component { }

    Health.schema = {
        current: { type: ecsy.Types.Number },
        max: { type: ecsy.Types.Number }
    };

    class Weapon extends ecsy.Component { }

    Weapon.schema = {
        damage: { type: ecsy.Types.Number }
    };

    world.registerComponent(Health);
    world.registerComponent(Weapon);

    var entityA = world.createEntity()
        .addComponent(Health, { current: 10, max: 10 })
        .addComponent(Weapon, { damage: 3 });

    var entityB = world.createEntity()
        .addComponent(Health, { current: 10, max: 10 });

    class AttackedSystem extends ecsy.System {
        init() { }
        execute(delta, time) { }
    }

    AttackedSystem.queries = {
        attacked: {
            components: [Health, ecsy.Not(Weapon)]
        }
    }

    class AttackSystem extends ecsy.System {
        init() { }
        execute(delta, time) {
            this.queries.attackers.results.forEach(attacker => {
                world.getSystem(AttackedSystem).queries.attacked.results.forEach(attacked => {
                    var dmg = attacker.getComponent(Weapon).damage;
                    attacked.getMutableComponent(Health).current -= dmg;
                });
            });
        }
    }

    AttackSystem.queries = {
        attackers: {
            components: [Health, Weapon]
        }
    }

    world
    .registerSystem(AttackSystem)
    .registerSystem(AttackedSystem);

    while (entityB.getComponent(Health).current > 0){
        console.log("Before: Attacked health: " + entityB.getComponent(Health).current + "/" + entityB.getComponent(Health).max);
        world.execute();
        console.log("After: Attacked health: " + entityB.getComponent(Health).current + "/" + entityB.getComponent(Health).max);
    }
    console.log("entityB is dead.");
}

myFunction();