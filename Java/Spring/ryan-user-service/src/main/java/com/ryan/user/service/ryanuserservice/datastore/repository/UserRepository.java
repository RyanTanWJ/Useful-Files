package com.ryan.user.service.ryanuserservice.datastore.repository;

import com.ryan.user.service.ryanuserservice.datastore.document.User;
import org.springframework.data.mongodb.repository.MongoRepository;

import java.util.List;

public interface UserRepository extends MongoRepository<User, Integer> {

    List<User> findByUserName(String userName);
}
