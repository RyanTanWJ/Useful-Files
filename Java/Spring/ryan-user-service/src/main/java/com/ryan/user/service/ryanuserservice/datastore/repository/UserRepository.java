package com.ryan.user.service.ryanuserservice.datastore.repository;

import com.ryan.user.service.ryanuserservice.datastore.document.Users;
import org.springframework.data.mongodb.repository.MongoRepository;

public interface UserRepository extends MongoRepository<Users, Integer> {
}
