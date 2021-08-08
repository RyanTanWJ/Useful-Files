package com.ryan.user.service.ryanuserservice.datastore.repository;

import com.ryan.user.service.ryanuserservice.datastore.document.User;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.data.mongodb.repository.Query;

import java.util.List;

public interface UserRepository extends MongoRepository<User, Integer> {

    List<User> findByUserName(String userName);

    List<User> deleteByUserId(String userId);
}
