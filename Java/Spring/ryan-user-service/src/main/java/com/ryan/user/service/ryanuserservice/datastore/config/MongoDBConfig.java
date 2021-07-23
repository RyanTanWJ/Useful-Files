package com.ryan.user.service.ryanuserservice.datastore.config;

import com.ryan.user.service.ryanuserservice.datastore.document.Users;
import com.ryan.user.service.ryanuserservice.datastore.repository.UserRepository;
import org.springframework.boot.CommandLineRunner;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.data.mongodb.repository.config.EnableMongoRepositories;

// TODO: Move to service level?

@EnableMongoRepositories(basePackageClasses = UserRepository.class)
@Configuration
public class MongoDBConfig {

//    @Bean
//    CommandLineRunner commandLineRunner(UserRepository userRepository){
//        return new CommandLineRunner() {
//            @Override
//            public void run(String... args) throws Exception {
//                userRepository.save(new Users(1,"Ryan", "this_is_ryan", System.currentTimeMillis()/1000L));
//            }
//        };
//    }
}
