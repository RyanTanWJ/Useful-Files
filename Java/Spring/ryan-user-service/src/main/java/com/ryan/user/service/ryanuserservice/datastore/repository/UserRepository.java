package com.ryan.user.service.ryanuserservice.datastore.repository;

import com.ryan.user.service.ryanuserservice.datastore.document.User;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.data.mongodb.repository.Query;

import java.util.List;
import java.util.Optional;

public interface UserRepository extends MongoRepository<User, Integer> {

    List<User> findByUserName(String userName);

    List<User> deleteByUserId(String userId);

    @Query("{'name':?0}") // write the query here
    Optional<User> findByName(String name);
}
